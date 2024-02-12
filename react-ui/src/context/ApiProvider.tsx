import { createContext, useEffect, useState } from "react"
import Axios, { AxiosInstance, AxiosResponse, InternalAxiosRequestConfig, isAxiosError } from "axios";

const baseUrl = 'https://localhost:5000'

interface IApiContext {
	axiosInstance: AxiosInstance,
	authorised: boolean,
	setAuthorised: (a: boolean) => void
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
	if (isAxiosError(error) && error.code == "ERR_NETWORK") {
		return Promise.resolve(error);
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

export const ApiContext = createContext<IApiContext>({
	axiosInstance: Axios.create({ baseURL: baseUrl }),
	authorised: false,
	setAuthorised: () => { }
});

function ApiProvider({ children }: any) {
	const axiosInstance = axiosDefault();
	const [authorised, setAuthorised] = useState(isAuthorised());

	return (
		<ApiContext.Provider value={{ axiosInstance, authorised, setAuthorised }}>
			{children}
		</ApiContext.Provider>);
}

export default ApiProvider;
