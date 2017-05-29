# asb-selfpoll-usingwebjobs
This solution is used to self poll azure service bus using webjobs sdk

### Configurations:
Update App.config for below values

	<add key="Microsoft.ServiceBus.ConnectionString" value="YOUR SERVICEBUS ENDPOINT HERE" />
    <add key="Microsoft.WebJobs.StorageString" value="YOUR BLOB STORAGE ENDPOINT HERE" />  
		
###
More details can be found at the blog post: http://rumanblogs.com/azure-service-bus-self-polling-health-check/
  
### Contributing
All pull requests are welcome
