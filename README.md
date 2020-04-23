# evtx-to-elk
Visual Basic UI to help send evtx files to the ELK Stack. The program uses Winlogbeat to send the logs based on a custom config file.

## Installation
To install and use the program you need to download the repo and open it with Visual Studio. Once openend, run the program or build it.

## Usage
The executable is very simple to use and has a ver symple UI. The executable also checks for all the inputs in order to make sure that you can't crash it.

![Script](https://i.imgur.com/Hehlr4B.png)

## Custom config
The custom config is a Yaml file that needs to contain 2 things.

1. Mandatory part which looks like this

![Mandatory part](https://i.imgur.com/rJljxv3.png)

2. Custom part where you choose where winlogbeat has to send the evtx files

![Custom part](https://i.imgur.com/7M3h3Ba.png)

My file sends data to Kafka topic winevent. This part is completely customised and should be changed to send it to where you need to send it. (Elasticsearch, Kibana, Logstash, etc)
