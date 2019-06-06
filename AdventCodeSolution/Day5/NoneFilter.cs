﻿namespace AdventCodeSolution.Day5
{
    public class NoneFilter : IUnitFilter
    {
        private NoneFilter()
        {

        }

        public bool CanBeAdded(char unit) => true;

        public static NoneFilter Value { get; } = new NoneFilter();
    }
}
