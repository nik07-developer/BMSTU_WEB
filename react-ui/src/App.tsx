import HomePage from './pages/HomePage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import PlayerScreen from './pages/PlayerScreen';
import ApiProvider from './context/ApiProvider';
import { useState } from 'react';
import { Character, CharacterData } from './@types/Model';

const sample_character_data: CharacterData = {
	maxhealth: 24,
	attributes: [
		{ name: "strength", name_short: "str", value: 1 },
		{ name: "dexterity", name_short: "dex", value: 2 },
		{ name: "constitution", name_short: "con", value: 3 },
		{ name: "intelligence", name_short: "int", value: 4 },
		{ name: "wisdom", name_short: "wis", value: 5 },
		{ name: "charisma", name_short: "cha", value: 6 }
	]
};
const sample_character: Character = {
	name: "cc",
	data: sample_character_data,
	screens: []
};

function App() {
	// Character data is shared between player screen and character selection.
	//const [characterList, setCharacterList] = useState<Character[]>([]);
	const [characterActive, setCharacterActive] = useState<Character | undefined>(sample_character);

	//setCharacterActive(sample_character);

	return (
		<ApiProvider>
			<Router>
				<Routes>
					<Route path="/" element={<HomePage />} />
					<Route path="/login" element={<LoginPage />} />
					<Route path="/register" element={<RegisterPage />} />
					<Route path="/player-screen" element={<PlayerScreen character={characterActive} />} />
				</Routes>
			</Router>
		</ApiProvider>
	);
}

export default App;
