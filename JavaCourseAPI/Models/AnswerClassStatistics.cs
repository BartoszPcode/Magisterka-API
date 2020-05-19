using System;
using System.Collections.Generic;

namespace JavaCourseAPI.Models
{
    public partial class AnswerClassStatistics
    {
        public AnswerClassStatistics()
        {
            ExerciseAnswer = new HashSet<ExerciseAnswer>();
        }

        public int IdAnswerClassStatistics { get; set; }
        public int McCabeComplexity { get; set; }
        public int TotalMethods { get; set; }
        public int StaticMethods { get; set; }
        public int PublicMethods { get; set; }
        public int FinalMethodes { get; set; }
        public int ProtectedMethods { get; set; }
        public int DefaultMethods { get; set; }
        public int AbstractMethodes { get; set; }
        public int SynchronizedMethods { get; set; }
        public int TotalFields { get; set; }
        public int StaticFields { get; set; }
        public int PublicFields { get; set; }
        public int PrivateFields { get; set; }
        public int ProtectedFileds { get; set; }
        public int DefaultFields { get; set; }
        public int FinalFields { get; set; }
        public int SynchronizedFields { get; set; }
        public int StaticInvocations { get; set; }
        public int LinesOfCode { get; set; }
        public int ReturnQty { get; set; }
        public int LoopQty { get; set; }
        public int ComparisonQty { get; set; }
        public int TryCatchQty { get; set; }
        public int StringLiteralsQty { get; set; }
        public int NumbersQty { get; set; }
        public int AssignmentsQty { get; set; }
        public int MathOperationsQty { get; set; }
        public int VariablesQty { get; set; }
        public int MaxNestedBlocks { get; set; }
        public int AnonymousClassQty { get; set; }
        public int LambdasQty { get; set; }
        public int UniqueWordsQty { get; set; }
        public int Modifiers { get; set; }
        public int LogStatementsQty { get; set; }

        public virtual ICollection<ExerciseAnswer> ExerciseAnswer { get; set; }
    }
}
