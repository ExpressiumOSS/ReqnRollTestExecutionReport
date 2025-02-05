﻿using Expressium.TestExecution;
using Expressium.TestExecutionReport.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Expressium.TestExecutionReport
{
    internal class TestExecutionReportDataGenerator
    {
        internal List<string> GenerateProjectDataListViewSections(TestExecutionProject project)
        {
            List<string> listOfLines = new List<string>();

            listOfLines.Add("<!-- Project Data ListView Section -->");
            listOfLines.Add($"<div class='data-item' id='listview'>");

            listOfLines.Add("<div class='section'>");
            listOfLines.Add("<table id='scenariolist' class='grid'>");

            listOfLines.Add("<thead>");
            listOfLines.Add("<tr role='header'>");
            listOfLines.Add("<th align='center' onClick='sortTableByColumn(0)'>&nbsp;#&nbsp;</th>");
            listOfLines.Add("<th onClick='sortTableByColumn(1)'>Feature<span class='sort-column'>&udarr;</span></th>");
            listOfLines.Add("<th onClick='sortTableByColumn(2)'>Scenario<span class='sort-column'>&udarr;</span></th>");
            listOfLines.Add("<th onClick='sortTableByColumn(3)'>Seqence<span class='sort-column'>&udarr;</span></th>");
            listOfLines.Add("<th onClick='sortTableByColumn(4)'>Duration<span class='sort-column'>&udarr;</span></th>");
            listOfLines.Add("</tr>");
            listOfLines.Add("</thead>");

            listOfLines.Add("<tbody id='scenario-list'>");

            foreach (var feature in project.Features)
            {
                foreach (var scenario in feature.Scenarios)
                {
                    listOfLines.Add($"<tr class='gridlines' tags='{feature.Title} {feature.GetTags()} {scenario.GetTags()}' onclick=\"loadScenario('{feature.Id}','{scenario.Id}');\">");

                    listOfLines.Add($"<td align='center'>");
                    listOfLines.Add($"<span class='status-dot bgcolor-{scenario.GetStatus().ToLower()}'></span>");
                    listOfLines.Add("</td>");

                    listOfLines.Add($"<td>");
                    listOfLines.Add($"<span><a href='#'>&nbsp;{feature.Title}</a></span>");
                    listOfLines.Add("</td>");

                    listOfLines.Add($"<td>");
                    listOfLines.Add($"<span><a href='#'>&nbsp;{scenario.Title}</a></span>");
                    listOfLines.Add("</td>");

                    listOfLines.Add($"<td align='right'>{scenario.Index}</td>");
                    listOfLines.Add($"<td align='right'>{scenario.GetDuration()}</td>");
                    listOfLines.Add($"</tr>");
                }
            }

            listOfLines.Add("</tbody>");
            listOfLines.Add("</table>");
            listOfLines.Add("</div>");

            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateProjectDataTreeViewSections(TestExecutionProject project)
        {
            List<string> listOfLines = new List<string>();

            listOfLines.Add("<!-- Project Data TreeView Section -->");
            listOfLines.Add($"<div class='data-item' id='treeview'>");

            listOfLines.Add("<div class='section'>");
            listOfLines.Add("<table id='scenariolist' class='grid'>");

            listOfLines.Add("<tbody id='scenario-list'>");

            foreach (var feature in project.Features)
            {
                listOfLines.Add($"<tr role='feature' style='color: dimgray;'>");
                listOfLines.Add($"<td style='width: 8px;'><b>&#10011;<b></td>");
                listOfLines.Add($"<td colspan='2' style='border-bottom: 1px solid lightgray;'>");

                //listOfLines.Add($"<span class='status-dot bgcolor-{feature.GetStatus().ToLower()}'></span>");
                listOfLines.Add($"<span><b>{feature.Title}</b></span>");

                //if (feature.GetNumberOfPassed() > 0)
                //    listOfLines.Add($"<span class='status-dot bgcolor-passed'></span>");

                //if (feature.GetNumberOfIncomplete() > 0)
                //    listOfLines.Add($"<span class='status-dot bgcolor-incomplete'></span>");

                //if (feature.GetNumberOfFailed() > 0)
                //    listOfLines.Add($"<span class='status-dot bgcolor-failed'></span>");

                //if (feature.GetNumberOfSkipped() > 0)
                //    listOfLines.Add($"<span class='status-dot bgcolor-skipped'></span>");

                listOfLines.Add($"</td>");
                listOfLines.Add($"</tr>");

                foreach (var scenario in feature.Scenarios)
                {
                    listOfLines.Add($"<tr role='scenario' tags='{feature.Title} {scenario.GetStatus()} {feature.GetTags()} {scenario.GetTags()}' onclick=\"loadScenario('{feature.Id}','{scenario.Id}');\">");
                    listOfLines.Add($"<td style='width: 8px;'></td>");
                    listOfLines.Add($"<td style='width: 24px;'></td>");
                    listOfLines.Add($"<td style='border-bottom: 1px solid lightgray;'>");
                    listOfLines.Add($"<span class='status-dot bgcolor-{scenario.GetStatus().ToLower()}'></span>");
                    listOfLines.Add($"<a href='#'>{scenario.Title}</a>");
                    listOfLines.Add($"</td>");
                    listOfLines.Add($"</tr>");
                }
            }

            listOfLines.Add("</tbody>");
            listOfLines.Add("</table>");
            listOfLines.Add("</div>");

            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateFeatureDataSections(TestExecutionProject project)
        {
            var listOfLines = new List<string>();

            foreach (var feature in project.Features)
            {
                listOfLines.Add("<!-- Feature Data Section -->");
                listOfLines.Add($"<div class='data-item' id='{feature.Id}'>");

                listOfLines.Add("<div class='section'>");
                listOfLines.AddRange(GenerateFeatureDataTagSection(feature));
                listOfLines.AddRange(GenerateFeatureDataNameSection(feature));
                listOfLines.AddRange(GenerateFeatureDataDescriptionSection(feature));
                listOfLines.Add("</div>");

                listOfLines.Add("</div>");
            }

            return listOfLines;
        }

        internal List<string> GenerateFeatureDataTagSection(TestExecutionFeature feature)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Feature Data Tag Section -->");
            listOfLines.Add("<div>");
            listOfLines.Add("<span class='tag-names'>" + feature.GetTags() + "</span>");
            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateFeatureDataNameSection(TestExecutionFeature feature)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Feature Data Name Section -->");
            listOfLines.Add("<div>");
            listOfLines.Add($"<span class='status-dot bgcolor-{feature.GetStatus().ToLower()}'></span>");
            listOfLines.Add($"<span class='feature-name'>Feature: {feature.Title}</span>");
            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateFeatureDataDescriptionSection(TestExecutionFeature feature)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Feature Data Description Section -->");
            listOfLines.Add("<div class='feature-description'>");
            var listOfDescription = feature.Description.Trim().Split("\n");
            foreach (var line in listOfDescription)
                listOfLines.Add("<span>" + line.Trim() + "</span><br />");
            listOfLines.Add("</div>");
            listOfLines.Add("<p></p>");

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataSections(TestExecutionProject project)
        {
            var listOfLines = new List<string>();

            foreach (var feature in project.Features)
            {
                foreach (var scenario in feature.Scenarios)
                {
                    listOfLines.Add("<!-- Scenario Data Section -->");
                    listOfLines.Add($"<div class='data-item' id='{scenario.Id}'>");

                    bool exampleSplitter = false;
                    foreach (var example in scenario.Examples)
                    {
                        if (exampleSplitter)
                            listOfLines.Add("<hr>");
                        exampleSplitter = true;

                        listOfLines.Add("<!-- Scenario Outline Section -->");
                        listOfLines.Add("<div class='section'>");

                        listOfLines.AddRange(GenerateScenarioDataTagSection(scenario));

                        listOfLines.Add("<table>");
                        listOfLines.Add("<tbody>");
                        listOfLines.AddRange(GenerateScenarioDataTitleSection(scenario, example));
                        listOfLines.AddRange(GenerateScenarioDataStepsSection(example));
                        listOfLines.AddRange(GenerateScenarioDataExamplesSection(example));
                        listOfLines.AddRange(GenerateScenarioDataMessageSection(example));
                        listOfLines.Add("</tbody>");
                        listOfLines.Add("</table>");
                        listOfLines.Add("</div>");

                        listOfLines.AddRange(GenerateScenarioDataAttachments(example));
                    }

                    listOfLines.Add("</div>");
                }
            }

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataTagSection(TestExecutionScenario scenario)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Scenario Data Tag Section -->");
            listOfLines.Add("<div>");
            listOfLines.Add("<span class='tag-names'>" + scenario.GetTags() + "</span>");
            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataTitleSection(TestExecutionScenario scenario, TestExecutionExample example)
        {
            var scenarioKeyword = "Scenario:";
            if (example.Arguments.Count > 0)
                scenarioKeyword = "Scenario Outline:";

            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Scenario Data Title Section -->");
            listOfLines.Add("<tr>");
            listOfLines.Add("<td style='padding-left: 0px;' colspan='2'>");
            listOfLines.Add($"<span class='status-dot bgcolor-{example.GetStatus().ToLower()}'></span>");
            listOfLines.Add("<span class='scenario-name'>" + scenarioKeyword + " " + scenario.Title + " </span>");
            listOfLines.Add("<span class='duration'>&nbsp;" + example.GetDuration() + "</span>");
            listOfLines.Add("</td>");
            listOfLines.Add("</tr>");

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataStepsSection(TestExecutionExample example)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Scenario Data Steps Section -->");

            foreach (var step in example.Steps)
            {
                var status = step.GetStatus().ToLower();

                var stepMarker = "";
                if (step.IsPassed())
                    stepMarker = "&check;";
                else
                    stepMarker = "&cross;";

                listOfLines.Add($"<tr>");
                listOfLines.Add($"<td colspan='2' style='padding-left: 0px;'>");
                listOfLines.Add($"<span class='step-indent color-{status}'><b>{stepMarker}</b></span>");
                listOfLines.Add($"<span class='step-keyword'> " + step.Type + "</span> ");
                listOfLines.Add($"<span>" + step.Text + "</span>");
                listOfLines.Add($"</td>");
                listOfLines.Add($"</tr>");

                if (step.Arguments.Count > 0)
                {
                    listOfLines.Add("<!-- Scenario Steps Table Section -->");
                    listOfLines.Add($"<tr>");
                    listOfLines.Add($"<td colspan='2' class='examples' style='padding-left: 64px;'>");

                    listOfLines.Add("<table>");
                    listOfLines.Add("<tbody>");

                    listOfLines.Add($"<tr>");
                    foreach (var argument in step.Arguments)
                        listOfLines.Add($"<td><i>| " + argument.Name + "</i></td>");
                    listOfLines.Add($"<td>|</td>");
                    listOfLines.Add($"</tr>");

                    listOfLines.Add($"<tr>");
                    foreach (var argument in step.Arguments)
                        listOfLines.Add($"<td>| " + argument.Value + "</td>");
                    listOfLines.Add($"<td>|</td>");
                    listOfLines.Add($"</tr>");

                    listOfLines.Add("</tbody>");
                    listOfLines.Add("</table>");

                    listOfLines.Add($"</td>");
                    listOfLines.Add($"</tr>");
                }
            }

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataExamplesSection(TestExecutionExample example)
        {
            var listOfLines = new List<string>();

            if (example.Arguments.Count > 0)
            {
                listOfLines.Add("<!-- Scenario Examples Section -->");
                listOfLines.Add($"<tr>");
                listOfLines.Add($"<td colspan='2' class='examples' style='padding-left: 0px;'><b>Examples:</b></td>");
                listOfLines.Add($"</tr>");

                listOfLines.Add($"<tr>");
                listOfLines.Add($"<td colspan='2' class='examples' style='padding-left: 0px;'>");

                listOfLines.Add("<table>");
                listOfLines.Add("<tbody>");

                listOfLines.Add($"<tr>");
                foreach (var argument in example.Arguments)
                    listOfLines.Add($"<td><i>| " + argument.Name + "</i></td>");
                listOfLines.Add($"<td>|</td>");
                listOfLines.Add($"</tr>");

                listOfLines.Add($"<tr>");
                foreach (var argument in example.Arguments)
                    listOfLines.Add($"<td>| " + argument.Value + "</td>");
                listOfLines.Add($"<td>|</td>");
                listOfLines.Add($"</tr>");

                listOfLines.Add("</tbody>");
                listOfLines.Add("</table>");

                listOfLines.Add($"</td>");
                listOfLines.Add($"</tr>");
            }

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataMessageSection(TestExecutionExample example)
        {
            var listOfLines = new List<string>();

            var status = example.GetStatus().ToLower();

            string message = null;
            if (example.Error != null)
                message = example.Error;
            else if (example.IsStepPending())
                message = "Pending Step Definition";
            else if (example.IsStepUndefined())
                message = "Undefined Step Definition";
            else if (example.IsStepBindingError())
                message = "Binding Error Step Definition";
            else
            {
            }

            if (message != null)
            {
                listOfLines.Add("<!-- Scenario Data Message Section -->");
                listOfLines.Add($"<tr><td></td></tr>");
                listOfLines.Add($"<tr>");
                listOfLines.Add($"<td style='padding-left: 0px;' colspan='2'>");
                listOfLines.Add($"<div class='step-{status}'>{message}</div>");
                listOfLines.Add($"</td>");
                listOfLines.Add($"</tr>");
            }

            return listOfLines;
        }

        internal List<string> GenerateScenarioDataAttachments(TestExecutionExample example)
        {
            var listOfLines = new List<string>();

            if (example.Attachments.Count > 0)
            {
                listOfLines.Add("<!-- Scenario Data Attachments Section -->");
                listOfLines.Add("<div class='section' style='padding-top: 2px; padding-bottom: 0px;'>");

                listOfLines.Add("<span class='scenario-name'>Attachments:</span>");
                listOfLines.Add("<ul>");

                foreach (var attachment in example.Attachments)
                {
                    var filePath = Path.GetFileName(attachment);
                    listOfLines.Add($"<li><a target='_blank' href='./Attachments/{filePath}'>{filePath}</a></li>");
                }

                listOfLines.Add("</ul>");

                listOfLines.Add("</div>");
            }

            return listOfLines;
        }

        internal List<string> GenerateProjectDataAnalyticsSection(TestExecutionProject project)
        {
            var listOfLines = new List<string>();

            listOfLines.Add("<!-- Project Data Analytics Section -->");
            listOfLines.Add($"<div class='data-item' id='analytics'>");

            listOfLines.Add("<div class='section'>");
            listOfLines.Add("<span class='project-name'>Analytics</span>");
            listOfLines.Add("</div>");

            listOfLines.AddRange(GenerateProjectDataAnalyticsStatusChartSection(project));
            listOfLines.Add("<p></p>");
            listOfLines.AddRange(GenerateProjectDataAnalyticsCoverageSection(project));
            listOfLines.Add("<p></p>");
            //listOfLines.AddRange(GenerateProjectStatusProperties(project));

            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateProjectDataAnalyticsStatusChartSection(TestExecutionProject project)
        {
            List<string> listOfLines = new List<string>();

            var numberOfPassed = project.GetNumberOfPassed();
            var numberOfIncomplete = project.GetNumberOfIncomplete();
            var numberOfFailed = project.GetNumberOfFailed();
            var numberOfSkipped = project.GetNumberOfSkipped();
            var numberOfTests = project.GetNumberOfTests();

            var numberOfPercentPassed = (int)Math.Round(100.0f / numberOfTests * numberOfPassed);
            var numberOfPercentIncomplete = (int)Math.Round(100.0f / numberOfTests * numberOfIncomplete);
            var numberOfPercentFailed = (int)Math.Round(100.0f / numberOfTests * numberOfFailed);
            var numberOfPercentSkipped = (int)Math.Round(100.0f / numberOfTests * numberOfSkipped);

            var sumOfPercent = numberOfPercentPassed + numberOfPercentIncomplete + numberOfPercentFailed + numberOfPercentSkipped;
            if (sumOfPercent > 100)
            {
                if (numberOfPercentPassed > 1)
                    numberOfPercentPassed -= 1;
                else if (numberOfPercentIncomplete > 1)
                    numberOfPercentIncomplete -= 1;
                else if (numberOfPercentFailed > 1)
                    numberOfPercentFailed -= 1;
                else if (numberOfPercentSkipped > 1)
                    numberOfPercentSkipped -= 1;
                else
                {
                }
            }

            {
                listOfLines.Add("<!-- Project Data Analytics Status Chart Section -->");
                listOfLines.Add("<div class='section' style='text-align: center; max-width: 500px; margin: auto;'>");
                listOfLines.Add($"<span class='chart-percentage'>{numberOfPercentPassed.ToString("0")}%</span><br />");
                listOfLines.Add("<span class='chart-status'>Passed</span><br />");

                listOfLines.Add("<p></p>");

                listOfLines.Add($"<div style='width: 100%; height: 0.8em;'>");
                listOfLines.Add($"<div class='bgcolor-passed' style='width: {numberOfPercentPassed}%; height: 0.8em; float: left'></div>");
                listOfLines.Add($"<div class='bgcolor-incomplete' style='width: {numberOfPercentIncomplete}%; height: 0.8em; float: left'></div>");
                listOfLines.Add($"<div class='bgcolor-failed' style='width: {numberOfPercentFailed}%; height: 0.8em; float: left'></div>");
                listOfLines.Add($"<div class='bgcolor-skipped' style='width: {numberOfPercentSkipped}%; height: 0.8em; float: left'></div>");
                listOfLines.Add("</div>");

                var message = GetStatusMessage((int)numberOfPercentPassed);
                listOfLines.Add($"<span style='color: gray; font-style: italic; margin: 8px; '>{message}</span>");
                listOfLines.Add("</div>");
            }

            {
                listOfLines.Add("<!-- Status Slave Chart Section -->");
                listOfLines.Add("<div class='section' style='text-align: center; max-width: 600px; margin: auto;'>");
                listOfLines.Add("<table align='center'>");
                listOfLines.Add("<tr>");

                listOfLines.Add("<td class='color-passed chart-count'>");
                listOfLines.Add($"<span class='chart-count-number'>{numberOfPassed}</span><br />");
                listOfLines.Add($"<span class='chart-count-percentage'>{numberOfPercentPassed}%</span><br />");
                listOfLines.Add($"<span class='chart-count-status'>Passed</span><br />");
                listOfLines.Add($"<div class='bgcolor-passed' style='width: 110px; height: 0.4em;'></div>");
                listOfLines.Add("</td>");

                listOfLines.Add("<td class='color-incomplete chart-count'>");
                listOfLines.Add($"<span class='chart-count-number'>{numberOfIncomplete}</span><br />");
                listOfLines.Add($"<span class='chart-count-percentage'>{numberOfPercentIncomplete}%</span><br />");
                listOfLines.Add($"<span class='chart-count-status'>Incomplete</span><br />");
                listOfLines.Add($"<div class='bgcolor-incomplete' style='width: 110px; height: 0.4em;'></div>");
                listOfLines.Add("</td>");

                listOfLines.Add("<td class='color-failed chart-count'>");
                listOfLines.Add($"<span class='chart-count-number'>{numberOfFailed}</span><br />");
                listOfLines.Add($"<span class='chart-count-percentage'>{numberOfPercentFailed}%</span><br />");
                listOfLines.Add($"<span class='chart-count-status'>Failed</span><br />");
                listOfLines.Add($"<div class='bgcolor-failed' style='width: 110px; height: 0.4em;'></div>");
                listOfLines.Add("</td>");

                listOfLines.Add("<td class='color-skipped chart-count'>");
                listOfLines.Add($"<span class='chart-count-number'>{numberOfSkipped}</span><br />");
                listOfLines.Add($"<span class='chart-count-percentage'>{numberOfPercentSkipped}%</span><br />");
                listOfLines.Add($"<span class='chart-count-status'>Skipped</span><br />");
                listOfLines.Add($"<div class='bgcolor-skipped' style='width: 110px; height: 0.4em;'></div>");
                listOfLines.Add("</td>");

                listOfLines.Add("<td class='color-total chart-count'>");
                listOfLines.Add($"<span class='chart-count-number'>{numberOfTests}</span><br />");
                listOfLines.Add($"<span class='chart-count-percentage'>100%</span><br />");
                listOfLines.Add($"<span class='chart-count-status'>Total</span><br />");
                listOfLines.Add($"<div style='background-color: black; width: 110px; height: 0.4em;'></div>");
                listOfLines.Add("</td>");

                listOfLines.Add("</tr>");
                listOfLines.Add("</table>");
                listOfLines.Add("</div>");
            }

            return listOfLines;
        }

        internal List<string> GenerateProjectDataAnalyticsCoverageSection(TestExecutionProject project)
        {
            List<string> listOfLines = new List<string>();

            listOfLines.Add("<!-- Project Data Analytics Coverage Section -->");
            listOfLines.Add("<div class='section' style='max-width: 600px; margin: auto;'>");
            listOfLines.Add("<span class='feature-name'>Features</span><br />");
            listOfLines.Add("<table class='grid' width='100%' align='center'>");
            listOfLines.Add("<thead>");
            listOfLines.Add("<tr>");
            listOfLines.Add("<th align='center'></th>");
            listOfLines.Add("<th>Name</th>");
            listOfLines.Add("<th align='center'>Total</th>");
            listOfLines.Add("<th align='center'>Coverage</th>");
            listOfLines.Add("<th>Status</th>");
            listOfLines.Add("</tr>");
            listOfLines.Add("</thead>");
            listOfLines.Add("<tbody>");

            foreach (var feature in project.Features)
            {
                var percentOfPassed = (int)Math.Round(100.0f / feature.GetNumberOfTests() * feature.GetNumberOfPassed());

                listOfLines.Add($"<tr onclick=\"presetScenarios('{feature.GetStatus().ToLower()}')\">");
                listOfLines.Add($"<td align='center'><span class='status-dot bgcolor-{feature.GetStatus().ToLower()}'></span></td>");
                listOfLines.Add($"<td>{feature.Title}</td>");
                listOfLines.Add($"<td align='center'>{feature.GetNumberOfTests()}</td>");
                listOfLines.Add($"<td align='center'>{percentOfPassed}%</td>");
                listOfLines.Add($"<td>{feature.GetStatus()}</td>");
                listOfLines.Add($"</tr>");
            }

            listOfLines.Add("</tbody>");
            listOfLines.Add("</table>");
            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal List<string> GenerateProjectDataStatusProperties(TestExecutionProject project)
        {
            List<string> listOfLines = new List<string>();

            listOfLines.Add("<!-- Status Project Data Properties Section -->");
            listOfLines.Add("<div class='section' style='max-width: 600px; margin: auto;'>");
            listOfLines.Add("<span class='feature-name'>Properties</span><br />");
            listOfLines.Add("<table width='100%' align='center' border='1'>");
            listOfLines.Add("<thead>");
            listOfLines.Add("<tr><th>Name</th><th>Value</th></tr>");
            listOfLines.Add("</thead>");
            listOfLines.Add("<tbody>");
            listOfLines.Add($"<tr><td><b>Project: </b></td><td>{project.Title}</td></tr>");
            listOfLines.Add($"<tr><td><b>Execution Time: </b></td><td>{project.GetExecutionTime()}</td></tr>");
            listOfLines.Add($"<tr><td><b>Duration: </b></td><td>{project.GetDuration()}</td></tr>");
            listOfLines.Add($"<tr><td><b>Environment: </b></td><td>{project.Environment}</td></tr>");
            listOfLines.Add("</tbody>");
            listOfLines.Add("</table>");
            listOfLines.Add("</div>");

            return listOfLines;
        }

        internal string GetStatusMessage(int status)
        {
            if (status == 100)
                return "The system is fully covered and successfully validated!";
            else if (status >= 90)
                return "The system is extensively covered with minor potential risks!";
            else if (status >= 75)
                return "The system is well covered with significant potential risks!";
            else if (status >= 50)
                return "The system is moderately covered with significant potential risks!";
            else if (status >= 25)
                return "The system is partially covered with many potential risks!";
            else if (status >= 10)
                return "The system is minimally covered with many undetected risks!";
            else if (status < 10)
                return "The system is not covered with a uncertainties in reliability!";
            else
            {
            }

            return null;
        }
    }
}
