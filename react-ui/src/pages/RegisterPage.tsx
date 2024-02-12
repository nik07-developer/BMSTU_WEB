import { Button, Stack, TextField, Typography } from "@mui/material";
import { FormEvent } from "react";

function RegisterPage() {
    const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
    }

    return (
        <Stack component="form" onSubmit={onSubmit} spacing={3} sx={{ m: "auto", mt: "16vh", alignItems: "center" }}>
            <Typography component="h1" variant="h4">
                Регистрация
            </Typography>
            <TextField required type="email" name="email" label="Электронная почта" />
            <TextField required name="username" label="Имя пользователя" />
            <TextField required type="password" name="password" label="Пароль" />
            <TextField required type="password" name="password-confirm" label="Подтверждение пароля" />
            <Button type="submit" variant="contained">Зарегистрироваться</Button>
        </Stack>
    )
}

export default RegisterPage;
