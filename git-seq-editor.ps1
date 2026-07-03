param([string]$path)
(Get-Content $path) -replace '^pick ', 'edit ' | Set-Content -Path $path -Encoding UTF8
