Param(
  [parameter(Mandatory=$false)][string]$repo="http://192.168.85.1:8081/nuget",
  [parameter(Mandatory=$false)][bool]$push=$false,
	[parameter(Mandatory=$false)][string]$apikey,
	[parameter(Mandatory=$false)][bool]$build=$true
)

# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$slnPath = Join-Path $packFolder "../"
$srcPath = Join-Path $packFolder "../00.Surging.Core"


$projects = (
  "Surging.Core.ApiGateWay",
  "Surging.Core.Caching",
  "Surging.Core.Codec.MessagePack",
  "Surging.Core.Codec.ProtoBuffer",
  "Surging.Core.Common",
  "Surging.Core.Consul",
  "Surging.Core.CPlatform",
  "Surging.Core.DotNetty",
  "Surging.Core.EventBuspaKafka",
  "Surging.Core.EventBusRabbitMQ",
  "Surging.Core.KestrelHttpServer",
  "Surging.Core.Log4net",
  "Surging.Core.NLog",
  "Surging.Core.Protocol.Http",
  "Surging.Core.Protocol.WS",
  "Surging.Core.ProxyGenerator",
  "Surging.Core.ServiceHosting",
  "Surging.Core.Swagger",
  "Surging.Core.System",
  "Surging.Core.Zookeeper",
  "Surging.Core.Domain",
  "Surging.Core.Schedule",
  "Surging.Core.AutoMapper",
  "Surging.Core.Dapper"
)

function Pack($projectFolder,$projectName) {
  Set-Location $projectFolder
  $releaseProjectFolder = (Join-Path $projectFolder "bin/Release")
  if (Test-Path $releaseProjectFolder)
  {
     Remove-Item -Force -Recurse $releaseProjectFolder
  }
  
   & dotnet msbuild /p:Configuration=Release /p:SourceLinkCreate=true
   & dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true
   if ($projectName) {
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $projectName + ".*.nupkg")
   }else {
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $project + ".*.nupkg")
   }
   Move-Item -Force $projectPackPath $packFolder 
}

if ($build) {
  Set-Location $slnPath
  & dotnet restore SurgingDemo.sln

  foreach($project in $projects) {
    Pack -projectFolder (Join-Path $srcPath $project)
  }
  $webSocketProjectFolder = Join-Path $srcPath "WebSocketCore"   
  Pack -projectFolder $webSocketProjectFolder -projectName "Surging.WebSocketCore"
  Set-Location $packFolder
}

if($push) {
    if ([string]::IsNullOrEmpty($apikey)){
        Write-Warning -Message "未设置nuget仓库的APIKEY"
		exit 1
	}
	dotnet nuget push *.nupkg -s $repo -k $apikey
}
