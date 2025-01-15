# ReqnRoll Test Execution Report

SpecFlow has reached its end-of-life and we need a replacement
for the SpecFlow Living Doc functionality in ReqnRoll...

From my perspective, an MVP for a ReqnRoll Living Doc
should be a CLI program designed to post-process an HTML report,
similar to the current Living Doc functionality in SpecFlow.

This demo solution is intended to serve as input and inspiration for a potential final solution.

During test execution, an output file named TestAutomation.json will be generated,
containing the majority of the information needed to build a custom report generator
utilizing the FeatureContext and ScenarioContext classes. Additionally,
a lightweight TestExecutionReportGenerator will produce a simple HTML
report based on the TestAutomation.json file.

## How-To-Use
* Execute the ReqnRoll BDD business tests in the solution...
* Run the LivingDoc.bat file from the project's root folder...

## Download
GitHub: https://github.com/ExpressiumOSS/ReqnRollTestExecutionReport


