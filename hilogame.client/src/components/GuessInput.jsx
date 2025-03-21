import React, { useState } from 'react';
import axios from "axios";

function GuessInput({ playerName, gameId, isGameStarted, isPlayersTurn }) {

    const [guess, setGuess] = useState(""); // Player's guess
    const [message, setMessage] = useState('');

    // Handle the player's guess
    const makeGuess = async () => {
        if (!isGameStarted) {
            setMessage("Game has not started yet.");
            return;
        }

        const response = await axios.post("/api/game/guess", { gameId, guess: parseInt(guess), playerName });
        setMessage(response.data);
    };

    return (
        <div>
            <h3>Player: {playerName}</h3>
            <input
                type="number"
                value={guess}
                onChange={(e) => setGuess(e.target.value)}
                placeholder="Enter your guess"
                disabled={!isGameStarted || !isPlayersTurn} // Disable if the game hasn't started            />
            <button onClick={makeGuess} disabled={!isGameStarted || !isPlayersTurn}>Guess</button>
            <p>{message}</p>
        </div>
    );
}

export default GuessInput;