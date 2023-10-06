$username = "somnath"
$password = "Vm2kaPassword"

# Connect to the Azure VM using WinRM
$session = New-PSSession -ComputerName 40.81.246.149 -Credential (New-Object PSCredential -ArgumentList ($username, (ConvertTo-SecureString -String $password -AsPlainText -Force)))

# Copy the published app to the VM
Copy-Item -Path '.\publish\*' -Destination 'C:\inetpub\wwwroot' -ToSession $session

# Enter and exit the session, restart the app pool, etc.
Enter-PSSession -Session $session
Import-Module WebAdministration
Restart-WebAppPool -Name 'DefaultAppPool'
Exit-PSSession
Remove-PSSession -Session $session
