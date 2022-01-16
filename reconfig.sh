rm /home/helipoc/.local/share/data.db
dotnet ef migrations remove
dotnet ef migrations add initialCreate
dotnet ef database update
echo "[+] Database reconfigured"
