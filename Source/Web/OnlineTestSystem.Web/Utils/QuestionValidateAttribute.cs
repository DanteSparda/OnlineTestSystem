namespace OnlineTestSystem.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Web.ViewModels.Tests;

    public class QuestionValidateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as ICollection<AnswerCreateModel>;
            var hasCorrect = false;
            if (list.Count == 4)
            {
                foreach (var item in list)
                {
                    if (!hasCorrect && item.IsCorrect)
                    {
                        hasCorrect = true;
                    }
                    else if (hasCorrect && item.IsCorrect)
                    {
                        return false;
                    }
                }
            }

            return hasCorrect;
        }
    }
}
