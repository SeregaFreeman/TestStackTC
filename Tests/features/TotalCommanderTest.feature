Feature: TotalCommanderTest

Scenario: Basic total commander scenario
	Given Application was launched, folders "C:\\sub3", "C:\\sub4" with files "file1", "file2"  were created if was needed
	    And User clicks button with proper number to access app
	When User opens folder "sub3" in left panel, folder "sub4" in right panel and moves "file1" from left panel to right
	    And User confirms movement
