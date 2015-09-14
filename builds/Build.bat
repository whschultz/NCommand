call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"

nuget restore ../src/NCommand.sln
msbuild ../src/NCommand.sln /p:Configuration=Release /t:rebuild
nuget pack ../src/NCommand/NCommand.csproj -OutputDirectory "Packages" -Prop Configuration=Release
nuget pack ../src/NCommand/NCommand.csproj -OutputDirectory "Packages" -Symbols
nuget pack ../src/NCommand.Console/NCommand.Console.csproj -OutputDirectory "Packages" -Prop Configuration=Release
nuget pack ../src/NCommand.Console/NCommand.Console.csproj -OutputDirectory "Packages" -Symbols

pause
