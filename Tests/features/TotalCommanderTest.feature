Feature: TotalCommanderTest

Scenario: Basic total commander scenario
	Given The following folders were created if were absent:
	| Folder path |
	| C:\sub3     |
	| D:\sub4     |
	  And The following files were created in corresponding folders:
	  | Folder path | File name |
	  | C:\sub3     | file1     |
	  | D:\sub4     | file2     |

	When User opens the app
	  Then "Trial version welcome" window is opened

    When User clicks button with proper number to access app
	  Then "Main" window is opened

	When User opens following folders on corresponding panels:
	| Folder path | Panel |
	| C:\sub3     | left  |
	| D:\sub4     | right |
	  Then Following folders are open on corresponding panels:
	  | Folder path | Panel |
	  | C:\sub3     | left  |
	  | D:\sub4     | right |

    When User moves "file1" from "left" panel to "right" using drag and drop
	  Then Confirmation window is active

	When User confirms file movement
	  Then "file1" is present on "right" panel
	    And "file1" is present on "left" panel

	When User selects "Cut" option from context menu for "file1" on "right" panel
	  And User selects "Paste" option from context menu on "left" panel
	  Then "Replace or Skip Files" window is opened

	When User clicks "Replace" button on dialog window
	  Then "file1" is absent on "right" panel
	    And "file1" is present on "left" panel

	When User selects "Show/Separate Tree/1 (One For Both Panels)" from the main app menu
	  Then Side panel is opened

	When User clicks "2" times on "Switch through tree panel options" icon
	  Then Side panel is not opened

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
	  And User selects "Files/Edit Comment..." from the main app menu
	  Then Warning window is active

	When User closes warning dialog
	  Then Warning window is closed

    When User selects "Files/Quit" from the main app menu
	  Then App is closed