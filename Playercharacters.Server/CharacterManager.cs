using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NFive.SDK.Server.Communications;
using NFive.SDK.Server.IoC;
using Gaston11276.Playercharacters.Server.Models;
using Gaston11276.Playercharacters.Server.Events;
using Gaston11276.Playercharacters.Server.Storage;
using Gaston11276.Playercharacters.Shared;

namespace Gaston11276.Playercharacters.Server
{
	/// <summary>
	/// Wrapper library for accessing character data from external plugins.
	/// </summary>
	[Component(Lifetime = Lifetime.Singleton)]
	[PublicAPI]
	public class CharacterManager
	{
		private readonly ICommunicationManager comms;

		/// <summary>
		/// Occurs when a character session is being created for the clients selected character to play.
		/// </summary>
		public event EventHandler<CharacterEventArgs> Selecting;

		/// <summary>
		/// Occurs when a character session has been created for the clients selected character to play.
		/// </summary>
		public event EventHandler<CharacterSessionEventArgs> Selected;

		/// <summary>
		/// Occurs when a character session for a client's selected character is being disconnected.
		/// </summary>
		public event EventHandler<CharacterSessionEventArgs> Deselecting;

		/// <summary>
		/// Occurs when a character session for a client's selected character is disconnected.
		/// </summary>
		public event EventHandler<CharacterSessionEventArgs> Deselected;

		/// <summary>
		/// Gets the active character sessions.
		/// </summary>
		/// <value>
		/// The active character sessions.
		/// </value>
		public async Task<List<CharacterSession>> ActiveCharacterSessions() =>
			await this.comms.Event(PlayercharactersEvents.GetActive).ToServer().Request<List<CharacterSession>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="CharacterManager"/> class.
		/// </summary>
		/// <param name="comms"></param>
		public CharacterManager(ICommunicationManager comms)
		{
			this.comms = comms;
			//this.comms.Event(PlayercharactersEvents.Selecting).FromServer().On<Character>((e, c) => this.Selecting?.Invoke(this, new CharacterEventArgs(c)));
			//this.comms.Event(PlayercharactersEvents.Selected).FromServer().On<CharacterSession>((e, c) => this.Selected?.Invoke(this, new CharacterSessionEventArgs(c)));
			//this.comms.Event(PlayercharactersEvents.Deselecting).FromServer().On<CharacterSession>((e, c) => this.Deselecting?.Invoke(this, new CharacterSessionEventArgs(c)));
			//this.comms.Event(PlayercharactersEvents.Deselected).FromServer().On<CharacterSession>((e, c) => this.Deselected?.Invoke(this, new CharacterSessionEventArgs(c)));
		}

		/// <summary>
		/// Selects the specified character identifier as the active character.
		/// </summary>
		/// <param name="characterId">The character identifier.</param>
		/// <returns></returns>
		//public async Task<CharacterSession> Select(Guid characterId) => await this.comms.Event(PlayercharactersEvents.Select).ToServer().Request<CharacterSession>(characterId);

		public List<Container> GetCharacterInventories(Guid characterId)
		{
			using (var context = new StorageContext())
			{
				return context.CharacterInventories
					.Where(ci => ci.Character.Id == characterId)
					.Select(ci => ci.Container)
					.Include(c => c.Items.Select(i => i.ItemDefinition))
					.ToList();
			}
		}
	}
}
