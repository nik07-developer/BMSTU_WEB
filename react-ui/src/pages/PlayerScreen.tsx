import { useContext, useState } from "react";
import { Character, ScreenWidget, characterClone } from "../model/Model";
import { useUserLogout } from "../api/ApiHooks";
import Drawer from "@mui/material/Drawer"
import { styled, useTheme } from "@mui/material/styles";
import IconButton from "@mui/material/IconButton";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import MenuIcon from '@mui/icons-material/Menu';
import { List, ListItem, Typography, Button, Box, Divider } from "@mui/material";
import Draggable, { DraggableData, DraggableEvent } from "react-draggable";
import { htmlByWxName } from "../components/widgets/Widgets";

interface PlayerScreenProps {
	character: Character,
	setCharacter: (character: Character) => void;
}

function PlayerScreen({ character, setCharacter }: PlayerScreenProps) {
	const theme = useTheme();
	const [fetchError, onLogout] = useUserLogout();
	const [leftOpen, setLeftOpen] = useState(true);
	const [rightOpen, setRightOpen] = useState(true);
	const [activeScreen, setActiveScreen] = useState(0);

	return (
		<Box sx={{ display: 'flex' }}>
			<Box>
				<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(true); }}><MenuIcon /></IconButton>
				{character && (character.screens[activeScreen].widgets.map((wx: ScreenWidget, wxIndex: number) => {
					return (
						<Draggable defaultPosition={{ x: wx.posx, y: wx.posy }} onStop={(e: DraggableEvent, data: DraggableData) => {
							let chr = characterClone(character);
							chr.screens[activeScreen].widgets[wxIndex].posx = data.lastX;
							chr.screens[activeScreen].widgets[wxIndex].posy = data.lastY;
							setCharacter(chr);
						}}>
							<Box sx={{ position: "absolute", top: "0", left: "0" }}>{htmlByWxName(wx.name)(character, setCharacter)}</Box>
						</Draggable>)
				}))}
			</Box>
			<Drawer variant="persistent" anchor="left" open={leftOpen} sx={{ width: 250 }}>
				<Box display="flex" flexDirection="row" alignItems="center" sx={{ backgroundColor: theme.palette.secondary.main }}>
					<Typography variant="h5" margin={2}>Character Name</Typography>
					<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(false); }}>
						{theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
					</IconButton>
				</Box>
				<List sx={{ "& Button": { variant: "text", p: 1, m: -1, minWidth: 1, justifyContent: "left" } }}>
					<ListItem><Button>Войти</Button></ListItem>
					<ListItem><Button>Зарегистрироваться</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<ListItem><Button>В режим ГМ'а</Button></ListItem>
					<ListItem><Button>Выбор персонажа</Button></ListItem>
					<ListItem><Button>Экспорт в JSON</Button></ListItem>
					<Divider sx={{ m: 1 }} />
					<ListItem><Button>Редактировать экраны</Button></ListItem>
					<ListItem><Button>Выбор конфигурации</Button></ListItem>
				</List>
			</Drawer>
		</Box>
	)
}

export default PlayerScreen;
