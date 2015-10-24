cf push contoso-demo-v2 -b https://github.com/ryandotsmith/null-buildpack.git -s windows2012R2 --no-start
cf enable-diego contoso-demo-v2
cf set-health-check contoso-demo-v2 none
cf bind-service contoso-demo-v2 contoso-demo-sql
cf bind-service contoso-demo-v2 contoso-demo-queue
cf start contoso-demo-v2
