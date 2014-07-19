@Echo OFF
SET SolutionPath=SimuTraining.sln
Echo Start Time - %Time%
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild.exe -p:FrameworkPathOverride="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" %SolutionPath% /p:Configuration=Release
Echo End Time - %Time%
Set /p Wait=Build Process Completed...