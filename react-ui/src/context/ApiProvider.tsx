import { createContext, useEffect, useState } from "react"
import Axios, { AxiosError, AxiosInstance, AxiosResponse, InternalAxiosRequestConfig, isAxiosError } from "axios";

const baseUrl = 'http://localhost:5000'

interface IApiContext {
	axiosInstance: AxiosInstance,
	authorised: boolean,
	setAuthorised: (a: boolean) => void,
	getUsername: () => string | null,
}

function axiosDefault(): AxiosInstance {
	const result = Axios.create({ baseURL: baseUrl });

	result.interceptors.response.use(
		responseSuccessHandler,
		responseErrorHandler);

	result.interceptors.request.use(
		requestSuccessHandler,
		requestErrorHandler);

	return result;
}

//// INTERCEPTORS ////
function responseSuccessHandler(response: AxiosResponse<any, any>) {
	return response;
}

function responseErrorHandler(error: any) {
	if (isAxiosError(error)) {
		if (error.code == AxiosError.ERR_NETWORK) {
			return Promise.resolve(error);
		}
		else if (error.response?.status == 401) {
			localStorage.removeItem("token");
		}
	}
	return Promise.reject(error);
}

function requestSuccessHandler(requestConfig: InternalAxiosRequestConfig<any>) {
	const token = localStorage.getItem("token");

	if (token) {
		requestConfig.headers.Authorization = `Bearer ${token}`;
	}
	else {
		delete requestConfig.headers.Authorization;
	}

	return requestConfig;
}

function requestErrorHandler(error: any) {
	return Promise.reject(error);
}

function isAuthorised(): boolean {
	return localStorage.getItem("token") != null;
}

function getUsername() {
	return localStorage.getItem("username");
}

export const ApiContext = createContext<IApiContext>({
	axiosInstance: Axios.create({ baseURL: baseUrl }),
	authorised: false,
	setAuthorised: () => {},
	getUsername: getUsername,
});

function ApiProvider({ children }: any) {
	const axiosInstance = axiosDefault();
	const [authorised, setAuthorised] = useState(isAuthorised());

	return (
		<ApiContext.Provider value={{ axiosInstance, authorised, setAuthorised, getUsername }}>
			{children}
		</ApiContext.Provider>);
}

export default ApiProvider;
