interface Config {
  apiUrl: string;
}

const config: Config = {
  apiUrl: process.env.API_URL || 'https://localhost:7196/api/'
};

export default config;