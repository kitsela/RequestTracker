using RequestTracker.Common.Models;
using System.Collections.Concurrent;

namespace RequestTracker.Consumer.Services
{
    public class MultiThreadLogsWriter : IRequestInfoProcessor
    {
        private readonly ConcurrentQueue<string> _textBuffer = new ConcurrentQueue<string>();
        private readonly IDateTimeService _dateTimeService;
        private readonly string _filePath;

        //to have posibility stop logger if needed. It's not implemented in the currern version
        private CancellationTokenSource _source = new CancellationTokenSource();
        private CancellationToken _token;

        public MultiThreadLogsWriter(IDateTimeService dateTimeService, IConfiguration configuration)
        {
            _dateTimeService = dateTimeService;
            _filePath = configuration.GetValue<string>("LogsPath") ?? throw new NullReferenceException("LogsPath");
            _token = _source.Token;

            //run background job
            Task.Run(WriteToFile, _token);
        }

        public void Process(RequestInfo requestInfo)
        {
            var line = GetLine(requestInfo);
            _textBuffer.Enqueue(line);
        }

        //2022-12-19T14:16:49.9605280Z|https://google.com|SomeUserAgent 1.2.3|192.168.1.1
        private string GetLine(RequestInfo requestInfo)
        {
            return $"{_dateTimeService.UtcNow}|{NullPretifier(requestInfo.Referrer)}|{NullPretifier(requestInfo.UserAgent)}|{requestInfo.IpAddress}";
        }

        private static string NullPretifier(string str) => string.IsNullOrEmpty(str) ? "null" : str;

        private async void WriteToFile()
        {
            while (true)
            {
                if (_token.IsCancellationRequested)
                {
                    return;
                }

                CreatePathDirectories(_filePath);

                using (StreamWriter w = File.AppendText(_filePath))
                {
                    while (_textBuffer.TryDequeue(out string textLine))
                    {
                        await w.WriteLineAsync(textLine);
                    }
                    w.Flush();
                    await Task.Delay(100);
                }
            }
        }

        private static void CreatePathDirectories(string path)
        {
            try
            {
                var directories = Path.GetDirectoryName(path);
                if (directories != null && !Directory.Exists(directories))
                {
                    Directory.CreateDirectory(directories);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                throw;
            }
        }
    }
}