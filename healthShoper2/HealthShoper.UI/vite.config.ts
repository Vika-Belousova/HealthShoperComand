import { defineConfig } from 'vite'
import path from 'path';
import react from '@vitejs/plugin-react'
import EnvironmentPlugin from 'vite-plugin-environment';

// https://vite.dev/config/
export default defineConfig({
  server: {
    open: true,
    port: 3001,
  },
  preview: {
    open: true,
    port: 3001,
  },
  define: {
    'process.env.APP_VERSION': JSON.stringify(process.env.npm_package_version),
  },
    resolve: {
    alias: {
      components: path.resolve(__dirname, './src/components'),
      assets: path.resolve(__dirname, './src/assets'),
      infrastructure: path.resolve(__dirname, './src/infrastructure'),
      pages: path.resolve(__dirname, './src/pages'),
      utils: path.resolve(__dirname, './src/utils'),
      layouts: path.resolve(__dirname, './src/layouts'),
    },
  },
  plugins: [react(), EnvironmentPlugin('all', { prefix: 'REACT_APP_' })],
  css: {
    devSourcemap: true,
    preprocessorOptions: {
      scss: {
        additionalData: `
              @use "assets/styles/_variables.scss" as *;
              @use "assets/styles/_mixins.scss" as *;
        `,
      },
    },
  },
})
