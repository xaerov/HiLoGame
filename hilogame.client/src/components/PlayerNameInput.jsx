import React from "react";

function PlayerNameInput({ playerName, setPlayerName, setIsNameEntered }) {
    const handleNameSubmit = () => {
        if (playerName.trim()) {
            setIsNameEntered(true);
        } else {
            alert("Please enter a valid name.");
        }
    };

    return (
        <div>
            <input
                type="text"
                value={playerName}
                onChange={(e) => setPlayerName(e.target.value)}
                placeholder="Enter your name"
            />
            <button onClick={handleNameSubmit}>Submit</button>
        </div>
    );
}

export default PlayerNameInput;