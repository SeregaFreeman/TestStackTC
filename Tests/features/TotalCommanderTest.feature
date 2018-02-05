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
	  Then folder "C:\\sub4" is open in "right" panel

    When User moves "file1" from "left" panel to "right"
	  Then Confirmation window is open

	When User confirms file movement
	  Then File "file" is moved to folder "C:\\sub4" on "right" panel

	When User selects "Cut" option from context menu for "file1" on "right" panel
	  And User selects "Paste" option from context menu on "left" panel
	  Then Replace or skip files window is open

	When User clicks "Replace" button on dialog window
	  Then "file1" is absent on "right" panel
	    And "file1" is present on "left" panel

	When User selects "Show", "Separate Tree", "1 (One For Both Panels)" from the main app menu
	  Then Side panel is open

	When User clicks on "Switch through tree panel options" menu item
