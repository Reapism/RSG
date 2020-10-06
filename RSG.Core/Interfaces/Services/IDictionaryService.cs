using RSG.Core.Models;
using System.Threading.Tasks;

namespace RSG.Core.Interfaces.Services
{
    /// <summary>
    /// A service for adding, removing, and selecting <see cref="IRsgDictionary"/>
    /// from a sequence.
    /// </summary>
    public interface IDictionaryService
    {
        /// <summary>
        /// Adds an <see cref="IRsgDictionary"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IRsgDictionary"/> to add.</param>
        /// <returns>Whether the <see cref="IRsgDictionary"/> was added.</returns>
        void Add(IRsgDictionary dictionary);

        /// <summary>
        /// Removes an <see cref="IRsgDictionary"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IRsgDictionary"/> to remove.</param>
        /// <returns>Whether the <see cref="IRsgDictionary"/> was removed.</returns>
        bool Remove(IRsgDictionary dictionary);

        /// <summary>
        /// Selects a particular <see cref="IRsgDictionary"/>
        /// from an sequence asynchronously.
        /// </summary>
        /// <param name="dictionaryName">The name of a <see cref="IRsgDictionary"/> to select.</param>
        /// <returns>A Task that represents the selection process.</returns>
        Task SelectAsync(string dictionaryName);

        /// <summary>
        /// Returns a selected <see cref="RsgDictionary"/> which is fully constructed.
        /// </summary>
        /// <returns>Returns a task that represents a <see cref="RsgDictionary"/>
        /// which is fully constructed.</returns>
        Task<RsgDictionary> SelectedAsync();
    }
}