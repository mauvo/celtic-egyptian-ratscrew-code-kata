﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CelticEgyptianRatscrewKata.Game
{
    /// <summary>
    /// Represents the state of the game at any point.
    /// </summary>
    public class GameState
    {
        private readonly Cards m_Stack;
        private readonly IDictionary<string, Cards> m_Decks;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameState()
            : this(Cards.Empty(), new Dictionary<string, Cards>()) {}

        /// <summary>
        /// Constructor to allow setting the central stack.
        /// </summary>
        public GameState(Cards stack, IDictionary<string, Cards> decks)
        {
            m_Stack = stack;
            m_Decks = decks;
        }

        /// <summary>
        /// Add the given player to the game with the given deck.
        /// </summary>
        /// <exception cref="ArgumentException">If the given player already exists</exception>
        public void AddPlayer(string playerId, Cards deck)
        {
            if (m_Decks.ContainsKey(playerId)) throw new ArgumentException("Can't add the same player twice");
            m_Decks.Add(playerId, deck);
        }

        /// <summary>
        /// Play the top card of the given player's deck.
        /// </summary>
        public void PlayCard(string playerId)
        {
            Debug.Assert(m_Decks.ContainsKey(playerId));
            Debug.Assert(m_Decks[playerId].Any());

            var topCard = m_Decks[playerId].Pop();
            m_Stack.AddToTop(topCard);
        }

        /// <summary>
        /// Wins the stack for the given player.
        /// </summary>
        public void WinStack(string playerId)
        {
            Debug.Assert(m_Decks.ContainsKey(playerId));

            foreach (var card in m_Stack.Reverse())
            {
                m_Decks[playerId].AddToBottom(card);
            }
        }

        public bool HasCards(string playerId)
        {
            Debug.Assert(m_Decks.ContainsKey(playerId));
            return m_Decks[playerId].Any();
        }
    }
}