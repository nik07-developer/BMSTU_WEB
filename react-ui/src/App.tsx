import HomePage from './pages/HomePage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import PlayerScreen from './pages/PlayerScreen';
import ApiProvider from './context/ApiProvider';
import { useState } from 'react';
import { Character } from './model/Model';
import { characterCreate, characterGetAny, characterGetById, characterRemove, characterStore } from './model/ChracterList';
import CharacterSelectionPage from './pages/CharacterSelectionPage';
import { Guid } from 'guid-typescript';

function App() {
	//const [characters, setCharacters] = useState(characterGetAll());
	const [character, setCharacter] = useState(characterGetAny());

	const updateCharacter = (c: Character) => {
		if (characterStore(c, character.guid))
			setCharacter(c);
	}

	const selectCharacter = (guid: Guid) => {
		const c = characterGetById(guid);
		if (c)
			setCharacter(c);
	}

	const addCharacter = () => {
		characterCreate();
	}

	const removeCharacter = (guid: Guid) => {
		if (guid == character.guid) {
			characterRemove(guid);
			characterGetAny();
		}

		characterRemove(guid);
	}

	return (
		<ApiProvider>
			<Router>
				<Routes>
					<Route path="/" element={<HomePage />} />
					<Route path="/login" element={<LoginPage />} />
					<Route path="/register" element={<RegisterPage />} />
					<Route path="/player-screen" element={<PlayerScreen character={character} setCharacter={updateCharacter} />} />
					<Route path="/character-selection" element={
						<CharacterSelectionPage
							activeCharacter={character}
							setActiveCharacter={selectCharacter}
							characterAdd={addCharacter}
							characterRemove={removeCharacter} />} />
				</Routes>
			</Router>
		</ApiProvider>
	);
}

export default App;
