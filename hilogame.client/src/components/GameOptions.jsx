import React, { useState } from "react";

function GameOptions({ playerName, createGame, joinGame, setMessage }) {
    const [joinGameId, setJoinGameId] = useState("");

    const handleJoinGame = () => {
        if (!joinGameId) {
            setMessage("Please enter a valid Game ID.");
        } else {
            joinGame(joinGameId);
        }
    };

    return (
        <div>
            <h3>Player: {playerName}</h3>
            <button onClick={createGame}>Create Game</button>
            <p>or</p>
            <div>
                <input
                    type="number"
                    value={joinGameId}
                    onChange={(e) => setJoinGameId(e.target.value)}
                    placeholder="Enter Game ID to join"
                />
                <button onClick={handleJoinGame}>Join Game</button>
            </div>
        </div>
    );
}

export default GameOptions;