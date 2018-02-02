Feature: TotalCommanderTest

Scenario: Basic total commander scenario
	Given Folders "C:\\sub3", "C:\\sub4" with files "file1", "file2"  were created if was needed
	When User opens the app
	  Then Trial version window is open
    When User clicks button with proper number to access app
	  Then Main window is open
	When User opens folder "C:\\sub3" in "left" panel
	  Then folder "C:\\sub3" is open in "left" panel
	When User opens folder "C:\\sub4" in "right" panel
	  Then folder "C:\\sub4" is open in "left" panel
    When User moves "file1" from "left" panel to "right"
	  Then Confirmation window is open
	When User confirms file movement
	  Then File "file" is moved to folder "C:\\sub4" on "right" panel
