#
# Promote_Slot.ps1
#
Param(
	[string] $Name = "DDNToDo",
	[string] $SourceSlot,
    [string] $DestinationSlot
)

$secpasswd = ConvertTo-SecureString "P@ssw0rd" -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential ("demo@matteoemililive.onmicrosoft.com", $secpasswd)

Add-AzureAccount -Credential $cred

#Swap backend first and then frontend
Switch-AzureWebsiteSlot -Name "${Name}Api" -Slot1 $SourceSlot -Slot2 $DestinationSlot -Force -Verbose
Switch-AzureWebsiteSlot -Name $Name -Slot1 $SourceSlot -Slot2 $DestinationSlot -Force -Verbose