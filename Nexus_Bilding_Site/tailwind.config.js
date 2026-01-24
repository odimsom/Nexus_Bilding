/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        primary: '#8c92ab',
        'matte-base': '#E6E4DF',
        'matte-input': '#EDECEA',
        'matte-surface': '#DEDCD6',
        'matte-border': '#C7C5C0',
        'matte-text': '#4A4A4A',
        'matte-text-muted': '#727379',
        'ink-paid-bg': '#D4D9D4',
        'ink-paid-text': '#4A5A4A',
        'ink-paid-border': '#B8C4B8',
        'ink-pending-bg': '#E0DDD4',
        'ink-pending-text': '#5A5444',
        'ink-pending-border': '#C9C4B8',
        'ink-error-bg': '#DDD4D4',
        'ink-error-text': '#5A4444',
        'ink-error-border': '#C4B8B8',
      },
      fontFamily: {
        display: ['Inter', 'sans-serif'],
      },
      borderRadius: {
        DEFAULT: '0.375rem',
        lg: '0.5rem',
        xl: '0.75rem',
      },
    },
  },
  plugins: [],
};
