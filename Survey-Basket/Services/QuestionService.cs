using Microsoft.VisualBasic;
using Survey_Basket.Contracts.Answers;
using Survey_Basket.Contracts.Questions;
using SurveyBasket.Abstractions;
using SurveyBasket.Errors;
using System.Linq;

namespace Survey_Basket.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;

        public QuestionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<QuestionReponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var pollExists = await _context.Polls.AnyAsync(x => x.Id == pollId);
            if (!pollExists)
            {
                return Result.Failure<QuestionReponse>(PollErrors.PollNotFound);

            }
            var questionExists = await _context.Questions.AnyAsync(x => x.Content == request.Content && x.PollId == pollId);
            if (!questionExists)
            {
                return Result.Failure<QuestionReponse>(QuestionErrors.DuplicatedQuestionContent);
            }
            var question = request.Adapt<Question>();
            question.PollId= pollId;
            foreach (var answer in request.Answers)
            {
                question.Answers.Add(new Answer
                {
                    Content = answer.Content
                });
            }
            await _context.Questions.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(question.Adapt<QuestionReponse>()); 
        }

        public async Task<Result<IEnumerable<QuestionReponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var IPollExists = _context.Polls.AnyAsync(x => x.Id == pollId);
            if (!await IPollExists)
            {
                return Result.Failure<IEnumerable<QuestionReponse>>(PollErrors.PollNotFound);
            }
            var questions = await _context.Questions
              .Where(x => x.PollId == pollId)
              .Include(x => x.Answers)
              //.Select(q => new QuestionResponse(
              //    q.Id,
              //    q.Content,
              //    q.Answers.Select(a => new AnswerResponse(a.Id, a.Content))
              //))
              .ProjectToType<QuestionReponse>()
              .AsNoTracking()
              .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<QuestionReponse>>(questions);
        }

        public async Task<Result<QuestionReponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions
              .Where(x => x.PollId == pollId && x.Id == id)
              .Include(x => x.Answers)
              .ProjectToType<QuestionReponse>()
              .AsNoTracking()
              .SingleOrDefaultAsync(cancellationToken);

            if (question is null)
                return Result.Failure<QuestionReponse>(QuestionErrors.QuestionNotFound);

            return Result.Success(question);
        }
        public async Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var questionIsExists = await _context.Questions
                .AnyAsync(x => x.PollId == pollId
                    && x.Id != id
                    && x.Content == request.Content,
                    cancellationToken
                );

            if (questionIsExists)
                return Result.Failure(QuestionErrors.DuplicatedQuestionContent);

            var question = await _context.Questions
                .Include(x => x.Answers)
                .SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken);

            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            question.Content = request.Content;

            //current answers
            var currentAnswers = question.Answers.Select(x => x.Content).ToList();

            //add new answer
            var newAnswers = request.Answers.Select(x => x.Content).Where(x => !currentAnswers.Contains(x)).ToList();

            newAnswers.ForEach(answer =>
            {
                question.Answers.Add(new Answer { Content = answer });
            });

            question.Answers.ToList().ForEach(answer =>
            {
                answer.IsActive = request.Answers.Any(a => a.Content == answer.Content);
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(x=>x.PollId==pollId && x.Id==id );

            if (question is null)
                return Result.Failure(PollErrors.PollNotFound);

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }  

    }
}
