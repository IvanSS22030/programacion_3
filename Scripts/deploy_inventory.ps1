$psql = "C:\Program Files\PostgreSQL\17\bin\psql.exe"
$env:PGUSER = "postgres"
$env:PGPASSWORD = "Lucario2030*"

Write-Host "Deploying Inventory Table..."
& $psql -d pacientesdb -f "Scripts/create_inventory_table.sql" 2>&1 | Write-Host

if ($LASTEXITCODE -eq 0) {
    Write-Host "INVENTORY_DEPLOY_SUCCESS"
    exit 0
} else {
     Write-Error "Failed to deploy inventory table."
     exit 1
}
