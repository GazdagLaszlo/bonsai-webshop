import axios from "axios";

const errorMessages: Record<number, Record<string, string>> = {
  400: {
    "Old password is incorrect": "Helytelen jelenlegi jelszó!",
    "The given email address is already in use": "Ez az email cím már foglalt!",
    "Invalid credentials": "Érvénytelen adatok!",
    "Password is too weak": "A jelszó túl gyenge!",
    "Passwords do not match": "A jelszavak nem egyeznek!",
    "Invalid input": "Érvénytelen bevitel!",
    default: "Hibás adatok!",
  },

  401: {
    "Invalid or expired refresh token": "Lejárt vagy érvénytelen munkamenet!",
    Unauthorized: "Nincs jogosultságod ehhez a művelethez!",
    "Invalid email or password": "Helytelen email cím vagy jelszó!",
    default: "Hozzáférés megtagadva!",
  },

  404: {
    "User not found with id": "Felhasználó nem található!",
    "User not found": "Felhasználó nem található!",
    "Product not found": "A termék nem található!",
    "Order not found": "A rendelés nem található!",
    default: "A keresett adat nem található.",
  },

  409: {
    "Email already exists": "Ez az email már használatban van!",
    default: "Az adat már létezik.",
  },

  500: {
    "Server error": "Szerverhiba!",
  },
};

export function translateError(error: unknown): string {
  const defaultMessage = "Hiba történt!";

  if (!axios.isAxiosError(error)) return defaultMessage;

  const status = error.response?.status ?? 500;
  const backendMessage = error.response?.data?.message ?? "";

  const statusMessages = errorMessages[status];
  if (!statusMessages) return defaultMessage;

  return (
    statusMessages[backendMessage] ??
    statusMessages["default"] ??
    defaultMessage
  );
}
