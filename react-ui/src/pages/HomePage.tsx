import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { ApiContext } from "../context/ApiProvider";
import { Button, Box, Container, Stack, Grid, useTheme } from "@mui/material";
import { useLogout } from "../api/ApiLogon";

function HomePage() {
	const apiContext = useContext(ApiContext);
	const [fetchError, apiLogout] = useLogout();
	const navigate = useNavigate();

	const onLogout = async () => {
		await apiLogout();
		localStorage.removeItem("token");
		apiContext.setAuthorised(false);
	}
	const toLogin = () => { navigate("/login"); };
	const toRegister = () => { navigate("/register"); };
	const toPlayerScreen = () => { navigate("/player-screen"); };

	return (
		<Grid container direction="column" justifyContent="start">
			{apiContext.authorised ?
				<Stack alignItems="center" justifyContent="end" direction="row">
					<Button>sfdhglkalbgh</Button>
					<Button onClick={onLogout}>Выйти</Button>
				</Stack> :
				<Stack alignItems="center" justifyContent="end" direction="row">
					<Button variant="text" onClick={toLogin}>Войти</Button>
					<Button variant="text" onClick={toRegister}>Зарегистрироваться</Button>
				</Stack>}
			<Stack alignItems="center" justifyContent="center" direction="column" margin={4} >
				<Button sx={{ m: 3, minWidth: 300, minHeight: 70, fontSize: 36 }} variant="contained" onClick={toPlayerScreen}>Я игрок!</Button>
				<Button sx={{ m: 3, minWidth: 300, minHeight: 70, fontSize: 36 }} variant="contained">Я ГМ! todo</Button>
			</Stack>
		</Grid>
	)
}

export default HomePage;
