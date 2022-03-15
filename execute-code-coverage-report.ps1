#Requires -Version 7.0
$TestOutput = dotnet test --collect:"XPlat Code Coverage"
$CoverageReports = $TestOutput | Select-String coverage.cobertura.xml | ForEach-Object { $_.Line.Trim() } | Join-String -Separator ';'
#echo $TestOutput
#echo $CoverageReports
$result = reportgenerator "-reports:$CoverageReports" "-targetdir:./CoverageReport" "-reporttype:Html"
echo $result
Invoke-Expression ./coveragereport\index.html