import HomePage from './pages/HomePage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import PlayerScreen from './pages/PlayerScreen';
import ApiProvider from './context/ApiProvider';
import { useState } from 'react';
import { Character, CharacterData, SkillProficiency } from './@types/Model';

const sample_character_data: CharacterData = {
	max_health: 24,
	health: 10,
	level: 5,
	armor_class: 10,
	attributes: [
		{ name: "strength", value: 10 },
		{ name: "dexterity", value: 12 },
		{ name: "constitution", value: 8 },
		{ name: "intelligence", value: 13 },
		{ name: "wisdom", value: 17 },
		{ name: "charisma", value: 7 }
	],
	skills: [
		{ name: "acrobatics", proficiency: SkillProficiency.Trained },
		{ name: "athletics", proficiency: SkillProficiency.Expert },
		{ name: "perception", proficiency: SkillProficiency.Untrained },
		{ name: "survival", proficiency: SkillProficiency.Untrained },
		{ name: "animal_handling", proficiency: SkillProficiency.Untrained },
		{ name: "intimidation", proficiency: SkillProficiency.Untrained },
		{ name: "perfomance", proficiency: SkillProficiency.Untrained },
		{ name: "history", proficiency: SkillProficiency.Untrained },
		{ name: "sleight_of_hand", proficiency: SkillProficiency.Untrained },
		{ name: "magic", proficiency: SkillProficiency.Untrained },
		{ name: "medicine", proficiency: SkillProficiency.Untrained },
		{ name: "deception", proficiency: SkillProficiency.Untrained },
		{ name: "nature", proficiency: SkillProficiency.Untrained },
		{ name: "insight", proficiency: SkillProficiency.Untrained },
		{ name: "investigation", proficiency: SkillProficiency.Untrained },
		{ name: "religion", proficiency: SkillProficiency.Untrained },
		{ name: "stealth", proficiency: SkillProficiency.Untrained },
		{ name: "persuasion", proficiency: SkillProficiency.Untrained },
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
					<Route path="/player-screen" element={<PlayerScreen character={characterActive} setCharacter={setCharacterActive} />} />
				</Routes>
			</Router>
		</ApiProvider>
	);
}

export default App;
