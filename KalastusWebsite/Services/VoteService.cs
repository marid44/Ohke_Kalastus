using System.Threading.Tasks;
using KalastusWebsite.Data;
using KalastusWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace KalastusWebsite.Services
{
    // VoteService.cs
    public class VoteService
    {
        private readonly AppDbContext _context;

        public VoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddVoteAsync(int userId, int? conversationId, int? commentId, bool isUpvote)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Console.WriteLine($"Vote attempt - UserId: {userId}, ConversationId: {conversationId}, CommentId: {commentId}, IsUpvote: {isUpvote}");

                var existingVote = await _context.Votes
                    .FirstOrDefaultAsync(v => v.UserId == userId &&
                        (v.ConversationId == conversationId || v.CommentId == commentId));

                Console.WriteLine($"Existing vote found: {existingVote != null}, " +
                    (existingVote != null ? $"Previous vote type: {existingVote.IsUpvote}" : "No previous vote"));

                if (existingVote == null)
                {
                    _context.Votes.Add(new Vote
                    {
                        UserId = userId,
                        ConversationId = conversationId,
                        CommentId = commentId,
                        IsUpvote = isUpvote,
                        CreatedAt = DateTime.UtcNow
                    });
                    Console.WriteLine("Adding new vote to database");
                }
                else if (existingVote.IsUpvote != isUpvote)
                {
                    existingVote.IsUpvote = isUpvote;
                    _context.Votes.Update(existingVote);
                    Console.WriteLine($"Updating existing vote to: {isUpvote}");
                }
                else
                {
                    _context.Votes.Remove(existingVote);
                    Console.WriteLine("Removing existing vote");
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Log final vote counts
                if (conversationId.HasValue)
                {
                    var upvotes = await _context.Votes.CountAsync(v => v.ConversationId == conversationId && v.IsUpvote);
                    var downvotes = await _context.Votes.CountAsync(v => v.ConversationId == conversationId && !v.IsUpvote);
                    Console.WriteLine($"Updated conversation vote counts - Upvotes: {upvotes}, Downvotes: {downvotes}");
                }
                else if (commentId.HasValue)
                {
                    var upvotes = await _context.Votes.CountAsync(v => v.CommentId == commentId && v.IsUpvote);
                    var downvotes = await _context.Votes.CountAsync(v => v.CommentId == commentId && !v.IsUpvote);
                    Console.WriteLine($"Updated comment vote counts - Upvotes: {upvotes}, Downvotes: {downvotes}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing vote: {ex.Message}");
                await transaction.RollbackAsync();
                return false;
            }
        }
    }


}