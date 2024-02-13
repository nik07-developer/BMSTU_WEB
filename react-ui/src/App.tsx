import HomePage from './pages/HomePage';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import PlayerScreen from './pages/PlayerScreen';
import ApiProvider from './context/ApiProvider';
import { useState } from 'react';
import { Character } from './model/Model';
import { characterCreate, characterGetAll, characterGetAny, characterRemove, characterStore } from './model/ChracterList';
import CharacterSelectionPage from './pages/CharacterSelectionPage';

function App() {
	const [characters, setCharacters] = useState(characterGetAll());
	const [activeCharacter, setActiveCharacter] = useState(characters[0].name);

	const updateCharacter = (c: Character) => {
		if (c.name != "" && characterStore(c, activeCharacter)) {
			setCharacters(characterGetAll());
			setActiveCharacter(c.name);
		}
	}

	const selectCharacter = (name: string) => {
		setActiveCharacter(name);
	}

	const addCharacter = () => {
		characterCreate();
		setCharacters(characterGetAll());
	}

	const removeCharacter = (name: string) => {
		if (characters.length > 1) {
			characterRemove(name);
			setCharacters(characterGetAll());
			if (activeCharacter == name)
				setActiveCharacter(characters[0].name);
		}
	}

	const getCharacter = (name: string) => {
		return characters.find(c => c.name == name) || characters[0];
	}

	return (
		<ApiProvider>
			<Router>
				<Routes>
					<Route path="/" element={<HomePage />} />
					<Route path="/login" element={<LoginPage />} />
					<Route path="/register" element={<RegisterPage />} />
					<Route path="/player-screen" element={<PlayerScreen character={getCharacter(activeCharacter)} setCharacter={updateCharacter} />} />
					<Route path="/character-selection" element={
						<CharacterSelectionPage
							characters={characters}
							activeCharacter={getCharacter(activeCharacter)}
							setActiveCharacter={selectCharacter}
							characterAdd={addCharacter}
							characterRemove={removeCharacter} />} />
				</Routes>
			</Router>
		</ApiProvider>
	);
}

export default App;
