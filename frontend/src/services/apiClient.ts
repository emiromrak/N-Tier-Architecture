// Generic API Client
// Base URL for the NTier backend API
const BASE_URL = 'http://localhost:5211/api';

export type ApiResponse<T> =
  | { data: T; error: null }
  | { data: null; error: string };

async function request<T>(
  path: string,
  options?: RequestInit,
): Promise<ApiResponse<T>> {
  try {
    const res = await fetch(`${BASE_URL}${path}`, {
      headers: {
        'Content-Type': 'application/json',
        ...options?.headers,
      },
      ...options,
    });

    if (!res.ok) {
      const text = await res.text().catch(() => res.statusText);
      return { data: null, error: `HTTP ${res.status}: ${text}` };
    }

    // 204 No Content → return empty object cast to T
    if (res.status === 204) {
      return { data: {} as T, error: null };
    }

    const json = (await res.json()) as T;
    return { data: json, error: null };
  } catch (err) {
    const message = err instanceof Error ? err.message : String(err);
    return { data: null, error: message };
  }
}

export const apiClient = {
  get: <T>(path: string) => request<T>(path),
  post: <T>(path: string, body: unknown) =>
    request<T>(path, { method: 'POST', body: JSON.stringify(body) }),
  put: <T>(path: string, body: unknown) =>
    request<T>(path, { method: 'PUT', body: JSON.stringify(body) }),
  delete: <T>(path: string) => request<T>(path, { method: 'DELETE' }),
};
