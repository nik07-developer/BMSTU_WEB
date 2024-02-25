import { useContext, useState } from "react";
import { FetchError, FetchState } from "./ApiHooks";
import { ApiContext } from "../context/ApiProvider";
import { AxiosError, isAxiosError } from "axios";

function messageFromStatus(error: AxiosError<any, any>) {
    switch (error.code) {
        case undefined:
            return "Неизвестная ошибка";
        case AxiosError.ERR_BAD_REQUEST:
            return "Неверное имя пользователя и/или пароль";
    }
}

export function useLogin() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const login = async (username: string, password: string, setToken: (token: string) => void) => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/login', { username, password });
			const token = response.data.auth_token as string;
			setToken(token);
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e))
                setFetchError({state: FetchState.ERROR, message: messageFromStatus(e)});
			else if (e instanceof Error)
				setFetchError({ state: FetchState.ERROR, message: e.message });
			else
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
		}
	};

	return [fetchError, login] as const;
}

export function useLogout() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const logout = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/logout');
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e))
                setFetchError({state: FetchState.ERROR, message: messageFromStatus(e)});
			else if (e instanceof Error)
				setFetchError({ state: FetchState.ERROR, message: e.message });
			else
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
		}
	};

	return [fetchError, logout] as const;
}

export function useRefresh() {
	const [fetchError, setFetchError] = useState<FetchError>({ state: FetchState.DEFAULT });
	const ctx = useContext(ApiContext);

	const logout = async () => {
		try {
			setFetchError({ state: FetchState.LOADING });
			const response = await ctx.axiosInstance.post('/refresh');
			setFetchError({ state: FetchState.SUCCESS });
		}
		catch (e: unknown) {
			if (isAxiosError(e))
                setFetchError({state: FetchState.ERROR, message: messageFromStatus(e)});
			else if (e instanceof Error)
				setFetchError({ state: FetchState.ERROR, message: e.message });
			else
				setFetchError({ state: FetchState.ERROR, message: "Неизвестная ошибка" });
		}
	};

	return [fetchError, logout] as const;
}
