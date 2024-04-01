
It's just basic implementation without loggers. Configs are hardcoded etc.


I have created dockers for the API and Consumer projects
but there is some issue with rabitmq connection string, it doesnt work properly from docker container. 
I will play with it a bit later

To test project:
1. docker compose up => it will run rabitmq docker container

2. Select multiple startup projects on solution. Chose API and Consumer
3. Run it in debug mode 