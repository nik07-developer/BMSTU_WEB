import { FormEvent, useContext } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { FetchState } from "../api/ApiHooks";
import { TextField, Button, Typography, Stack, Alert, AlertTitle } from "@mui/material";
import { useLogin } from "../api/ApiLogon";
import { ApiContext } from "../context/ApiProvider";

function LoginPage() {
	const navigate = useNavigate();
	const prevRoute = useLocation();
	const [getUserError, getUser] = useLogin();
	const apiCtx = useContext(ApiContext);

	const onTokenReceive = (token: string) => {
		if (token) {
			localStorage.setItem("token", token);
			apiCtx.setAuthorised(true);
			navigate("/player-screen");
		}
		else {
			localStorage.removeItem("token");
			apiCtx.setAuthorised(false);
		}
	}

	const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault();

		const data = new FormData(e.currentTarget);
		const username = data.get("username") as string;
		const password = data.get("password") as string;

		await getUser(username, password, onTokenReceive);
	}

	return (
		<Stack component="form" onSubmit={onSubmit} spacing={3} sx={{ m: "auto", mt: "16vh", alignItems: "center"}}>
			<Typography component="h1" variant="h4">
				Вход
			</Typography>
			{getUserError.state == FetchState.ERROR &&
			<Alert variant="standard" severity="error" sx={{width: "223px"}}>
				<AlertTitle>Ошибка</AlertTitle>
				{getUserError.message}
			</Alert>
			}
			<TextField required name="username" label="Имя пользователя"/>
			<TextField type="password" required name="password" label="Пароль"/>
			<Button type="submit" variant="contained">Войти</Button>
		</Stack>
	)
}

export default LoginPage;
