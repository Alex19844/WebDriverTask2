# WebDriver. Task 2

Practical Task for Selenium WebDriver using NUnit framework.

## Task Description

Automate the following script:

1. Open https://pastebin.com/ or a similar service in any browser.
2. Create 'New Paste' with the following attributes:
   * Code:
            git config --global user.name  "New Sheriff in Town"
            git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
            git push origin master --force
   * Syntax Highlighting: "Bash"
   * Paste Expiration: "10 Minutes"
   * Paste Name / Title: "how to gain dominance among developers"
3. Save 'paste' and check the following:
   * Browser page title matches 'Paste Name / Title'
   * Syntax is suspended for bash
   * Check that the code matches the one from paragraph 2


 ## Task Implementation

 The "WebDriverTask2" project has been created. SeleniumTests.cs file has been added for tests.

 I faced a problem with bypassing the Selenium website security protection. So there are only 10 commits available per day.
 First I had to use an already created commit for testing. Then I used the ExpressVPN to successfully test my code.
 
 All functionality is implemented.
 
