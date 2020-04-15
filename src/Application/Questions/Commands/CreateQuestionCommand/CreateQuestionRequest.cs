using System.Collections.Generic;
using Codidact.Core.Application.Common.Contracts;

namespace Codidact.Core.Application.Questions.Commands.CreateQuestionCommand
{
    public class CreateQuestionRequest : IRequest
    {
        /// <summary>
        /// Title of the question
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Body of the question
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The tags of the question
        /// </summary>
        public IEnumerable<long> Tags { get; set; } = new List<long>();

        /// <summary>
        /// The category of the question
        /// </summary>
        public string Category { get; set; }
    }
}
