Feature: TotalCommanderTest

Scenario: Basic total commander scenario
	Given Folders "C:\\sub3", "C:\\sub4" with files "file1", "file2"  were created if was needed

	When User opens the app
	  Then Trial version window is open

    When User clicks button with proper number to access app
	  Then Main window is open

	When User opens folder "C:\sub3" in "left" panel
	  Then folder "C:\sub3" is open in "left" panel

	When User opens folder "C:\sub4" in "right" panel
	  Then folder "C:\sub4" is open in "right" panel

    When User moves "file1" from "left" panel to "right"
	  Then Confirmation window is open

	When User confirms file movement
	  Then "file1" is present on "right" panel
	    And "file1" is present on "left" panel

	When User selects "Cut" option from context menu for "file1" on "right" panel
	  And User selects "Paste" option from context menu on "left" panel
	  Then Replace or skip files window is open

	When User clicks "Replace" button on dialog window
	  Then "file1" is absent on "right" panel
	    And "file1" is present on "left" panel

	When User selects "Show" => "Separate Tree" => "1 (One For Both Panels)" from the main app menu
	  Then Side panel is open

	When User clicks "2" times on "Switch through tree panel options" icon
	  Then Side panel is not open

	When User makes "left" panel active
	  And User clicks "1" times on "Search" icon
	  Then "General" tab item is selected
	    And "Search in" field value is "C:\sub3"

	When User sets value "file1" to "Search for" field
	  And User checks ReGex checkbox
	    And User clicks Start search button
	  Then Only "C:\sub3\file1" is found

	When User closes search window
	   Then Search window is closed

	When User unselects all files on "left" panel
	  And User selects "Files", "Edit Comment..." from the main app menu
	  Then Warning window is open

	When User closes warning window
	  Then Warning window is closed

    When User selects "Files", "Quit" from the main app menu
	  Then App is closed