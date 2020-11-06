# SiteAvailabilityProcessor

# Flow Diagram

![Optional Text](Images/siteAvailabilityArch.png)

SiteAvailabilityProcessor

# TechStack : 
.Net Core Console Application
# Services Used : 
RabbitMq Message Queue, PostgreSql.
# Features : 

This Console application is continuoisly listning to RabbitMq Message Queue for Message.
On receiving Message,

1. Checking whether the given input site is up or not.
2. Insert the site availability results into postgresql which will be used to display on UI.

# Deployement
This API is deployed as WebJobs in Azure.

# Docker Support

Docker File
Docker Compose File

# Telemetry

AppInsights

![Optional Text](Images/SiteAvailablityTelemetry.png)


# SiteAvailabilityProcessor.Tests

Added Unit Tests for SiteAvailabilityProcessor API
