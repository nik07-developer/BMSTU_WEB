import { Button, Stack, TextField, Typography } from "@mui/material";
import { FormEvent } from "react";

function RegisterPage() {
    const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
    }

    return (
        <Stack component="form" onSubmit={onSubmit} spacing={3} sx={{ m: "auto", mt: "16vh", alignItems: "center" }}>
            <Typography component="h1" variant="h4">
                Зарегистрироваться
            </Typography>
            <TextField required name="username" label="Имя пользователя" />
            <TextField type="password" required name="password" label="Пароль" />
            <Button type="submit" variant="contained">Зарегистрироваться</Button>
        </Stack>
    )
}

export default RegisterPage;
