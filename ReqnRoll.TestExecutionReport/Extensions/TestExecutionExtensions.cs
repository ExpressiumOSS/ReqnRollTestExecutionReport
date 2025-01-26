﻿using ReqnRoll.TestExecution;
using System;

namespace ReqnRoll.TestExecutionReport.Extensions
{
    public enum ReportStatuses
    {
        Failed,
        Inconclusive,
        Skipped,
        Passed,
        Undefined
    }

    internal static class TestExecutionExtensions
    {
        public static bool IsPassed(this string step)
        {
            if (step == TestExecutionStatuses.OK.ToString())
                return true;
            return false;
        }

        public static bool IsFailed(this string step)
        {
            if (step == TestExecutionStatuses.TestError.ToString())
                return true;
            return false;
        }

        public static bool IsSkipped(this string step)
        {
            if (step == TestExecutionStatuses.Skipped.ToString())
                return true;
            return false;
        }

        public static bool IsInconclusive(this string step)
        {
            if (step.IsStepPending() || step.IsStepUndefined() || step.IsStepBindingError())
                return true;
            return false;
        }

        public static bool IsStepPending(this string step)
        {
            if (step == TestExecutionStatuses.StepDefinitionPending.ToString())
                return true;
            return false;
        }

        public static bool IsStepUndefined(this string step)
        {
            if (step == TestExecutionStatuses.UndefinedStep.ToString())
                return true;
            return false;
        }

        public static bool IsStepBindingError(this string step)
        {
            if (step == TestExecutionStatuses.BindingError.ToString())
                return true;
            return false;
        }

        public static string GetStatus(this string step)
        {
            if (step.IsPassed())
                return ReportStatuses.Passed.ToString();
            else if (step.IsSkipped())
                return ReportStatuses.Skipped.ToString();
            else if (step.IsSkipped())
                return ReportStatuses.Failed.ToString();
            else if (step.IsInconclusive())
                return ReportStatuses.Inconclusive.ToString();
            else
            {
                return ReportStatuses.Undefined.ToString();
            }
        }

        public static string FormatTags(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var tags = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tags.Length; i++)
                tags[i] = "@" + tags[i];

            return string.Join(' ', tags);
        }
    }
}
