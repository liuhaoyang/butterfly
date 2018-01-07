# Butterfly
A distributed tracing system and application performance management.

# Design
``` text
+----------------------------------------------------------------------------------+
|  Server                                                                          |
|                                                                                  | 
|  +-----------+   +-----------+   +-----------+   +-----------+   +-----------+   |
|  | Collector +--->    MQ     +--->  Consumer +--->  Storage  +--->   Web UI  |   |  
|  +-----------+   +-----------+   +-----------+   +-----------+   +-----------+   |
|                                                                                  |
+----------------------------------------------------------------------------------+
```
# Quickstart
* download [latest release](https://github.com/ButterflyAPM/butterfly/releases)
* extract `butterfly-server-preview-0.0.2.tar.gz`
* `cd butterfly-server-preview-0.0.2`
* `dotnet butterfly.server.dll`(.NET Core SDK >= 2.0.0)
* browse to [http://localhost:9618](http://localhost:9618) to find traces
