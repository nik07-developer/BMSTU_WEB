import { useContext, useState } from "react";
import { Character, ScreenWidget, Widget } from "../@types/Model";
import WxAttributes from "../components/widgets/WxAttributes"
import { ApiContext } from "../context/ApiProvider";
import { useUserLogout } from "../api/ApiHooks";
import Drawer from "@mui/material/Drawer"
import { styled, useTheme } from "@mui/material/styles";
import IconButton from "@mui/material/IconButton";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import MenuIcon from '@mui/icons-material/Menu';
import { List, ListItem, Typography, Button, Box, Divider } from "@mui/material";
import Draggable, { DraggableData, DraggableEvent } from "react-draggable";
import WxHealth from "../components/widgets/WxHealth";
import { CharacterData } from "../@types/Model";
import WxArmor from "../components/widgets/WxArmor";
import WxSkills from "../components/widgets/WxSkills";
import WxLevel from "../components/widgets/WxLevel";

interface PlayerScreenProps {
	character?: Character,
	setCharacter?: (character: Character) => void;
}

function PlayerScreen({ character, setCharacter }: PlayerScreenProps) {
	const theme = useTheme();
	const [fetchError, onLogout] = useUserLogout();
	const [leftOpen, setLeftOpen] = useState(true);
	const [rightOpen, setRightOpen] = useState(true);

	const updateCharacter = (data: CharacterData) => {
		if (character && setCharacter) {
			setCharacter({
				name: character.name,
				data: data,
				screens: character.screens
			})
		}
		else {
			console.info("no way!!! this should never happen :'(");
		}
	}

	const [widgets, setWidgets] = useState<ScreenWidget[]>([
		{
			widget: {
				getHtml: WxAttributes,
				name: "attributes-view"
			},
			posx: 300,
			posy: 20
		},
		{
			widget: {
				getHtml: WxHealth,
				name: "health-view"
			},
			posx: 300,
			posy: 120
		},
		{
			widget: {
				getHtml: WxArmor,
				name: "armor-view"
			},
			posx: 700,
			posy: 10
		},
		{
			widget: {
				getHtml: WxSkills,
				name: "armor-view"
			},
			posx: 500,
			posy: 200
		},
		{
			widget: {
				getHtml: WxLevel,
				name: "armor-view"
			},
			posx: 700,
			posy: 100
		}
	]);

	let newWidgets: ScreenWidget[] = [];

	return (
		<Box sx={{ display: 'flex' }}>
			<Box>
				<IconButton sx={{ m: 2 }} onClick={() => { setLeftOpen(true); }}><MenuIcon /></IconButton>
				{character && (widgets.map((w: ScreenWidget) => {
					return (
						<Draggable defaultPosition={{x: w.posx, y: w.posy}} onStop={(e: DraggableEvent, data: DraggableData) => {
							w.posx = data.lastX;
							w.posy = data.lastY;
						}}>
							<Box sx={{position: "absolute", top: "0", left: "0"}}>{w.widget.getHtml(character.data, updateCharacter)}</Box>
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

/*
<SidePanel leftSide={true}>
			{apiContext.authorised ?
				<div className="vertical-list">
					<button className="text-button">INSERT USERNAME HERE!</button>
					<button className="text-button" onClick={onLogout}>Выйти</button>
				</div> :
				<div className="vertical-list">
					<NavLink className="text-button" to="/login">Войти</NavLink>
					<NavLink className="text-button" to="/register">Зарегистрироваться</NavLink>
				</div>}
			<div style={{ height: 1, marginRight: 20, backgroundColor: "gray" }}></div>
			<button className="text-button">В режим ГМ'а</button>
			<button className="text-button">Выбор персонажа</button>
			<button className="text-button">Экспорт в JSON</button>
			<div style={{ height: 1, marginRight: 20, backgroundColor: "gray" }}></div>
			<button className="text-button">Редактировать экраны</button>
			<button className="text-button">Выбор конфигурации</button>
		</SidePanel>
		<SidePanel leftSide={false}>
			<button className="text-button">Бой</button>
			<button className="text-button">Исследование</button>
			<button className="text-button">Отдых</button>
		</SidePanel>
		*/
