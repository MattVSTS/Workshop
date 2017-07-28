param(
	$path,
	$service,
	$assembly,
	$slot
)
Write-Verbose -Verbose -Message "$path"
Write-Verbose -Verbose -Message "$service"
Write-Verbose -Verbose -Message "$assembly"
Write-Verbose -Verbose -Message "$slot"
#Import-Module "C:\Program Files (x86)\Microsoft SDKs\Azure\PowerShell\ServiceManagement\Azure\Azure.psd1"

$secpasswd = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential ("dsgtodo@memiliquestoutlook.onmicrosoft.com", $secpasswd)

Add-AzureAccount -Credential $cred

$Website = Get-AzureWebsite -Name $service -Slot $slot
Write-Verbose -Verbose -Message "$Website"

$MSDeployURL = $Website.EnabledHostNames[1]

$paramFile = '{0}\{1}.SetParameters.xml' -f $path, $assembly
$paramName = 'IIS Web Application Name'


Write-Verbose -Verbose -Message "$paramFile"
$xml = [xml](get-content $paramFile)
$nodes = $xml.SelectNodes("/parameters/setParameter")
foreach ($n in $nodes)
{
    if ($n.name -eq $paramName)
    {
        $n.value = $service
    }
}
$xml.Save($paramFile)

Write-Verbose -Verbose -Message "$MSDeployURL"
$MSDeployCMD = '{0}\{1}.deploy.cmd' -f $path, $assembly
Write-Verbose -Verbose -Message "$MSDeployCMD"
$MSDeployService = "https://{0}:443/msdeploy.axd" -f $MSDeployURL, $service
Write-Verbose -Verbose -Message "$MSDeployService"
$p1 = "/setParam:Name='{0}'" -f "IIS Web Application Name"
$p2 = ",Value=$service"
$p = $p1+$p2
$MSDeployCommand = "$MSDeployCMD /y /m:$MSDeployService -allowUntrusted /u:'{0}' /p:{1} /a:basic" -f $Website.PublishingUsername, $Website.PublishingPassword, $p
Write-Verbose -Verbose -Message "$MSDeployCommand"
Invoke-Expression -Command $MSDeployCommand