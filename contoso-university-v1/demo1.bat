cf push contoso-demo-v1 -s windows2012R2 --no-start
cf enable-diego contoso-demo-v1
cf set-health-check contoso-demo-v1 none
cf bind-service contoso-demo-v1 contoso-demo-sql
cf bind-service contoso-demo-v1 contoso-demo-queue
rem cf bind-service contoso-demo-v1 netlogger
cf start contoso-demo-v1
cf map-route contoso-demo-v1 zibb.co -n cdsg

