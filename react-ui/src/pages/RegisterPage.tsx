import { Alert, AlertTitle, Button, CircularProgress, Stack, TextField, Typography } from "@mui/material";
import { FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FetchState, useUserRegister } from "../api/ApiHooks";

function RegisterPage() {
    const [fetchError, register] = useUserRegister();
    const [error, setError] = useState("")
    const navigate = useNavigate();
    const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setError("");
        const data = new FormData(e.currentTarget);
		const email = data.get("email") as string;
		const username = data.get("username") as string;
		const password = data.get("password") as string;
		const password_confirm = data.get("password-confirm") as string;
        if (password != password_confirm)
            setError("Пароли должны совпадать");
        else {
            await register(username, password, email);
        }
    }

    return (
        <Stack component="form" onSubmit={onSubmit} spacing={3} sx={{ m: "auto", mt: "16vh", alignItems: "center" }}>
            <Typography component="h1" variant="h4">
                Регистрация
            </Typography>
            {(fetchError.state == FetchState.ERROR || error) &&
			<Alert variant="standard" severity="error" sx={{width: "223px"}}>
				<AlertTitle>Ошибка</AlertTitle>
				{fetchError.message || error}
			</Alert>
			}
            {fetchError.state == FetchState.SUCCESS &&
                <Alert variant="standard" severity="success" sx={{width: "223px"}}>
                    <AlertTitle>Успех</AlertTitle>
                    Пользователь успешно зарегистрирован
                </Alert>
            }
            <TextField required type="email" name="email" label="Электронная почта" />
            <TextField required name="username" label="Имя пользователя" />
            <TextField required type="password" name="password" label="Пароль" />
            <TextField required type="password" name="password-confirm" label="Подтверждение пароля" />
            {fetchError.state != FetchState.LOADING &&
                <Button type="submit" variant="contained">Зарегистрироваться</Button>}
            {fetchError.state == FetchState.LOADING && <CircularProgress/>}
        </Stack>
    )
}

export default RegisterPage;
